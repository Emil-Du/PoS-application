import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import Calendar from 'react-calendar'
import { FaRegClock } from "react-icons/fa";
import 'react-calendar/dist/Calendar.css';
import "./Reservation.css";
import { CreateReservation, GetReservations } from '../services/reservationService'
import { getCustomer, registerCustomer } from '../services/customerService'
import { getProviders } from "../services/providerService";
import { getServices } from "../services/serviceService";
import { getExactEmployee } from "../services/employeeService";
import { useEmployee } from '../contexts/EmployeeContext';
import Navbar from "../components/NavBar";

type ValuePiece = Date | null;
type Value = ValuePiece | [ValuePiece, ValuePiece];

export default function Reservation() {
    const { employee } = useEmployee();
    const [date, setDate] = useState<Value>(new Date());
    const [time, setTime] = useState<string>("[Not selected]");
    const [serviceId, setServiceId] = useState<number | null>(null);
    const [providerId, setProviderId] = useState<number | null>(null);

    const [provider, setProvider] = useState<string>("[Not selected]");
    const [service, setService] = useState<string>("[Not selected]");
    const [customerName, setCustomerName] = useState<string>("");
    const [customerPhone, setCustomerPhone] = useState<string>("");

    const [staff, setStaff] = useState<Array<any>>([]);
    const [services, setServices] = useState<Array<any>>([]);
    const navigate = useNavigate();
    const [unavailableTimes, setUnavailableTimes] = useState<string[]>([]);


    const isDisabledConfirmation =
        !date ||
        time === "[Not selected]" ||
        provider === "[Not selected]" ||
        service === "[Not selected]" ||
        !customerName.trim() ||
        !customerPhone.trim();

    useEffect(() => {

        if (!employee) {
            navigate("/");
            return;
        }

        interface Provider {
            providerId: number;
            employeeId: number;
            name: string;
            qualifiedServiceIds: number[];
        }

        interface Service {
            serviceId: number;
            productId: number;
            companyId: number;
            title: string;
            basePrice: {
                amount: number;
                currency: string;
            };
            durationMinutes: number;
            status: string;
        }

        const fetchData = async () => {
            try {
                const providersRes = await getProviders(employee.locationId);
                const filteredProviders = providersRes.data.filter((item: Provider) => item.qualifiedServiceIds.length > 0);
                setStaff(filteredProviders);

                const servicesRes = await getServices(employee.locationId);
                const filteredServices = servicesRes.filter((item: Service) => item.status.toLowerCase() == "available");
                setServices(filteredServices);
            }
            catch (err) {
                throw new Error("Error while retrieving providers and services.");
            }
        };

        fetchData();
    }, []);

        interface Reservation {
        id: number;
        serviceId: number;
        locationId: number;
        providerId: number;
        customerId: number;
        reservationTime: number;
        appointmentTime: number;
        status: string;
    }

    useEffect(() => {
        if (!date || date instanceof Array || !(date instanceof Date) || !employee) {
            setUnavailableTimes([]);
            return;
        }

        const fetchUnavailableTimes = async () => {
            try {
                const [hours, minutes] = time.split(":").map(Number);

                const appointmentDateTime = new Date(date);
                appointmentDateTime.setHours(hours, minutes, 0, 0);

                const selectedDateTime = Math.floor(appointmentDateTime.getTime() / 1000)
                
                const dayStart = new Date(date);
                dayStart.setHours(0, 0, 0, 0);

                const nextDayStart = new Date(dayStart);
                nextDayStart.setDate(dayStart.getDate() + 1);

                const employeeReservations = await GetReservations({locationId: employee.locationId, providerId: providerId ?? undefined, status: 'Active', from: Math.floor(dayStart.getTime() / 1000), to: Math.floor(nextDayStart.getTime() / 1000)});

                const isBooked = employeeReservations.reservations.some((r: Reservation) => 
                    r.providerId === providerId &&
                    r.appointmentTime === selectedDateTime
                );

                if(isBooked) setTime("[Not selected]");
                
                const bookedTimes = employeeReservations.reservations
                    .filter((r: Reservation) => r.providerId === providerId)
                    .map((r: Reservation) => {
                        const d = new Date(r.appointmentTime * 1000);
                        const hours = d.getHours();
                        const minutes = d.getMinutes().toString().padStart(2, "0");
                        return `${hours}:${minutes}`;
                    });

                setUnavailableTimes(bookedTimes);

            } catch (err) {
                setUnavailableTimes([]);
            }
        };

        fetchUnavailableTimes();
    }, [providerId, date]);


    const createReservation = async () => {
        if (!date || date instanceof Array || !(date instanceof Date)) return;

        const [hours, minutes] = time.split(":").map(Number);

        const appointmentDateTime = new Date(date);
        appointmentDateTime.setHours(hours, minutes, 0, 0);

        const unixTimestamp = Math.floor(appointmentDateTime.getTime() / 1000)

        const resGetCustomer = await getCustomer(customerName, customerPhone);

        let customerId: number;

        if (resGetCustomer.data.length === 1) {
            customerId = resGetCustomer.data[0].customerId
        }
        else if (resGetCustomer.data.length === 0) {
            const resRegisterCustomer = await registerCustomer(customerName, customerPhone);
            customerId = resRegisterCustomer.customerId
        }
        else {
            throw new Error("Multiple customers found.");
        }

        const providerEmployee = await getExactEmployee(providerId!);

        await CreateReservation(serviceId!, providerEmployee.data[0].locationId, providerId!, customerId!, unixTimestamp);

        setDate(new Date());
        setTime("[Not selected]");
        setProvider("[Not selected]");
        setService("[Not selected]");
        setCustomerName("");
        setCustomerPhone("");
        setProviderId(null);
        setServiceId(null);
    };


    return (
        <div className="layout">
            <Navbar />

            <div className="reservations-module">
                <div className="info-and-selection-tab">
                    <p>RESERVATIONS MODULE</p>
                </div>

                <div className="input-columns">
                    <div className="column">
                        <p>SELECT DATE & TIME</p>
                        <div className="calendar">
                            <Calendar onChange={setDate} value={date} />
                        </div>
                        <div className="smaller-p">TIME SLOTS</div>

                        <div className="time-slots-column">
                            <div className="time-slots-row">
                                <button disabled={unavailableTimes.includes("8:00")} onClick={() => setTime("8:00")} className={time === "8:00" ? "selected-button" : ""}> <FaRegClock /> 8:00</button>
                                <button disabled={unavailableTimes.includes("9:00")} onClick={() => setTime("9:00")} className={time === "9:00" ? "selected-button" : ""}> <FaRegClock /> 9:00</button>
                            </div>
                            <div className="time-slots-row">
                                <button disabled={unavailableTimes.includes("10:00")} onClick={() => setTime("10:00")} className={time === "10:00" ? "selected-button" : ""}> <FaRegClock /> 10:00</button>
                                <button disabled={unavailableTimes.includes("11:00")} onClick={() => setTime("11:00")} className={time === "11:00" ? "selected-button" : ""}> <FaRegClock /> 11:00</button>
                            </div>
                            <div className="time-slots-row">
                                <button disabled={unavailableTimes.includes("13:00")} onClick={() => setTime("13:00")} className={time === "13:00" ? "selected-button" : ""}> <FaRegClock /> 13:00</button>
                                <button disabled={unavailableTimes.includes("14:00")} onClick={() => setTime("14:00")} className={time === "14:00" ? "selected-button" : ""}> <FaRegClock /> 14:00</button>
                            </div>
                            <div className="time-slots-row">
                                <button disabled={unavailableTimes.includes("15:00")} onClick={() => setTime("15:00")} className={time === "15:00" ? "selected-button" : ""}> <FaRegClock /> 15:00</button>
                                <button disabled={unavailableTimes.includes("16:00")} onClick={() => setTime("16:00")} className={time === "16:00" ? "selected-button" : ""}> <FaRegClock /> 16:00</button>
                            </div>
                        </div>
                    </div>

                    <div className="column">
                        <p>SELECT STAFF & SERVICE</p>

                        <div className="smaller-p">STAFF MEMBER</div>

                        <div className="scroll-container">
                            {
                                staff.map((staffer, index) => (
                                    <button
                                        onClick={() => { setProvider(staffer.name); setProviderId(staffer.employeeId); }}
                                        className={`staff-button ${providerId === staffer.employeeId ? "selected-button" : ""}`}
                                        key={index}>
                                        <div className="staff-button-name">{staffer.name}</div>
                                    </button>
                                ))
                            }
                        </div>

                        <div className="smaller-p">SERVICE</div>

                        <div className="scroll-container">
                            {
                                services.map((srv, index) => (
                                    <button
                                        onClick={() => { setService(srv.title); setServiceId(srv.serviceId); }}
                                        className={`service-button ${serviceId === srv.serviceId ? "selected-button" : ""}`}
                                        key={index}>
                                        <div>{srv.title}</div> <div>{`${srv.basePrice.amount} ${srv.basePrice.currency}`}</div>
                                    </button>
                                ))
                            }
                        </div>

                    </div>
                    <div className="column">
                        <p>BOOKING SUMMARY</p>

                        <div className="smaller-p">Customer Name</div>
                        <input type="text" placeholder="Enter customer name..." value={customerName} onChange={(e) => setCustomerName(e.target.value)} />

                        <div className="smaller-p">Contact Phone Number</div>
                        <input type="text" placeholder="Enter phone number..." value={customerPhone} onChange={(e) => setCustomerPhone(e.target.value)} />
                        <div className="details">
                            <p>APPOINTMENT DETAILS</p>

                            <div className="details-data"> <div>Date:</div> <div>{date instanceof Date ? date.toLocaleDateString() : ""}</div> </div>
                            <div className="details-data"> <div>Time:</div> <div>{time}</div> </div>
                            <div className="details-data"> <div>Staff:</div> <div>{provider}</div> </div>
                            <div className="details-data"> <div>Service:</div> <div>{service}</div> </div>
                        </div>

                        <button className="confirmation-button" disabled={isDisabledConfirmation} onClick={createReservation}>Confirm Reservation</button>
                    </div>
                </div>
            </div>
        </div>
    );
}
