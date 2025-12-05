import { useState } from "react";
import "./Reservation.css";

export default function Reservation() {
    const [selectedDate, setSelectedDate] = useState<Date | null>(null);

    return (
        <div className="reservation-layout">
            <div className="area area-calendar">
                <h3>Select a Day</h3>
                <Calendar selectedDate={selectedDate} onSelectDate={setSelectedDate} />
            </div>

            <div className="area area-time">
                <h3>Select Time</h3>
                <div className="placeholder">Times will appear here</div>
            </div>

            <div className="area area-service">
                <h3>Service</h3>
                <div className="placeholder">Service list placeholder</div>
            </div>

            <div className="area area-provider">
                <h3>Provider</h3>
                <div className="placeholder">Provider list placeholder</div>
            </div>

            <div className="area area-form">
                <h3>Customer Info</h3>

                <form className="form">
                    <input type="text" placeholder="Full Name" />
                    <input type="tel" placeholder="Phone" />
                    <input type="email" placeholder="Email" />

                    <button type="submit">Confirm Reservation</button>
                </form>
            </div>
        </div>
    );
}

interface CalendarProps {
    selectedDate: Date | null;
    onSelectDate: (date: Date) => void;
}

function Calendar({ selectedDate, onSelectDate }: CalendarProps) {
    const today = new Date();
    const daysInMonth = new Date(
        today.getFullYear(),
        today.getMonth() + 1,
        0
    ).getDate();

    const days = Array.from({ length: daysInMonth }, (_, i) => i + 1);

    return (
        <div className="calendar">
            {days.map((day) => {
                const dateObj = new Date(today.getFullYear(), today.getMonth(), day);

                const isSelected =
                    selectedDate?.toDateString() === dateObj.toDateString();

                return (
                    <div
                        key={day}
                        className={`calendar-day ${isSelected ? "selected" : ""}`}
                        onClick={() => onSelectDate(dateObj)}
                    >
                        {day}
                    </div>
                );
            })}
        </div>
    );
}
