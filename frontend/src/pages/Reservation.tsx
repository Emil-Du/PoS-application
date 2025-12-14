import { useState } from "react";
import Calendar from 'react-calendar'
import { FaRegClock } from "react-icons/fa";
import 'react-calendar/dist/Calendar.css';
import "./Reservation.css";
import { CreateReservation } from '../services/reservationService'
import { getCustomer, registerCustomer } from '../services/customerService'

type ValuePiece = Date | null;
type Value = ValuePiece | [ValuePiece, ValuePiece];

export default function Reservation() {
    const [date, setDate] = useState<Value>(new Date());
    const [time, setTime] = useState<string>("[Not selected]");
    const [serviceId, setServiceId] = useState<number | null>(null);
    const [locationId, setLocationId] = useState<number | null>(null);
    const [providerId, setProviderId] = useState<number | null>(null);
    const [customerId, setCustomerId] = useState<number | null>(null);

    const [provider, setProvider] = useState<string>("[Not selected]");
    const [service, setService] = useState<string>("[Not selected]");
    const [customerName, setCustomerName] = useState<string>("");
    const [customerPhone, setCustomerPhone] = useState<string>("");

    const isDisabledConfirmation = 
    !date ||
    time === "[Not selected]" ||
    provider === "[Not selected]" ||
    service === "[Not selected]" ||
    !customerName.trim() ||
    !customerPhone.trim();

    //Temporary (maybe do api request at first and save here?)
    const staff = [
    { first_name: 'Maria', last_name: 'Maria', specialty: 'Haircut', employee_id: 0},
    { first_name: 'James', last_name: 'James', specialty: 'Massage', employee_id: 1},
    { first_name: 'Sarah', last_name: 'Sarah', specialty: 'Makeup', employee_id: 2},
    { first_name: 'Tom', last_name: 'Tom', specialty: 'Pedicure', employee_id: 3},
    { first_name: 'Jenny', last_name: 'Jenny', specialty: 'Hair colouring', employee_id: 4 },
    { first_name: 'Karen', last_name: 'Karen', specialty: 'Manicure', employee_id: 5 }
    ];

    const services = [
    { name: 'Haircut', price: 25, location_id: 0, product_id: 0},
    { name: 'Hair colouring', price: 50, location_id: 1, product_id: 1 },
    { name: 'Manicure', price: 30, location_id: 2, product_id: 2 },
    { name: 'Pedicure', price: 35, location_id: 3, product_id: 3 },
    { name: 'Massage', price: 60, location_id: 4, product_id: 4 },
    { name: 'Makeup', price: 45, location_id: 5, product_id: 5 }
    ];

    const createReservation = async () => {
        if (!date || date instanceof Array || !(date instanceof Date)) return;
        
        const [hours, minutes] = time.split(":").map(Number);

        const appointmentDateTime = new Date(date);
        appointmentDateTime.setHours(hours, minutes, 0, 0);

        const unixTimestamp = Math.floor(appointmentDateTime.getTime() / 1000)

        const resGetCustomer = await getCustomer(customerName, customerPhone);

        if (resGetCustomer.data.length === 1) {
            setCustomerId(resGetCustomer.data[0].id);
        } 
        else if (resGetCustomer.data.length === 0) {
            const resRegisterCustomer = await registerCustomer(customerName, customerPhone);

            setCustomerId(resRegisterCustomer.id);
        } 
        else {
            throw new Error("Multiple customers found.");
        }
        
        const resCreateReservation = await CreateReservation(serviceId!, locationId!, providerId!, customerId!, unixTimestamp);
        
        };


  return (
    <div className="layout">
        <div className="sidebar-placeholder"></div>

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
                            <button onClick={() => setTime("8:00")} className={time === "8:00" ? "selected-button" : ""}> <FaRegClock/> 8:00</button>
                            <button onClick={() => setTime("9:00")} className={time === "9:00" ? "selected-button" : ""}> <FaRegClock/> 9:00</button>
                        </div>
                        <div className="time-slots-row">
                            <button onClick={() => setTime("10:00")} className={time === "10:00" ? "selected-button" : ""}> <FaRegClock/> 10:00</button>
                            <button onClick={() => setTime("11:00")} className={time === "11:00" ? "selected-button" : ""}> <FaRegClock/> 11:00</button>
                        </div>
                        <div className="time-slots-row">
                            <button onClick={() => setTime("13:00")} className={time === "13:00" ? "selected-button" : ""}> <FaRegClock/> 13:00</button>
                            <button onClick={() => setTime("14:00")} className={time === "14:00" ? "selected-button" : ""}> <FaRegClock/> 14:00</button>
                        </div>
                        <div className="time-slots-row">
                            <button onClick={() => setTime("15:00")} className={time === "15:00" ? "selected-button" : ""}> <FaRegClock/> 15:00</button>
                            <button onClick={() => setTime("16:00")} className={time === "16:00" ? "selected-button" : ""}> <FaRegClock/> 16:00</button>
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
                                    onClick={() => {setProvider(`${staffer.first_name} ${staffer.last_name}`); setProviderId(staffer.employee_id); }}
                                    className={`staff-button ${providerId === staffer.employee_id ? "selected-button" : ""}`}
                                    key={index}> 
                                    <div className="staff-button-name">{staffer.first_name} {staffer.last_name}</div> <div className="staff-button-specialty">{staffer.specialty}</div> 
                                </button>
                            ))
                        }
                    </div>

                    <div className="smaller-p">SERVICE</div>

                    <div className="scroll-container">
                        {
                            services.map((srv, index) => (
                                <button 
                                    onClick={() => {setService(srv.name); setServiceId(srv.product_id); setLocationId(srv.location_id)}}
                                    className={`service-button ${serviceId === srv.product_id ? "selected-button" : ""}`}
                                    key={index}> 
                                    <div>{srv.name}</div> <div>{srv.price}€</div>
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
                    <input type="text" placeholder="Enter phone number..." value={customerPhone} onChange={(e) => setCustomerPhone(e.target.value)}/>
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
