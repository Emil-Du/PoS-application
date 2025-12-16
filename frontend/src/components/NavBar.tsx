import "./NavBar.css";
import { useEmployee } from "../contexts/EmployeeContext";
import { useNavigate } from "react-router-dom";
import { logout } from "../services/authService";


export default function Navbar() {
    const { employee, setEmployee } = useEmployee();

    if (!employee) return null;

    const isEmployee = employee.role.toLowerCase() === "employee";
    const navigate = useNavigate();

    const handleLogout = async () => {
        sessionStorage.removeItem('currentOrder');
        await logout();
        setEmployee(null);
        navigate("/");
    };

    return (
        <nav>
            <h2>PoS App</h2>
            <div className="nav-divider" />
            <button hidden={!employee.isFoodLocationEmployee} onClick={() => navigate("/home")}>Home</button>
            <button hidden={employee.isFoodLocationEmployee} onClick={() => navigate("/reservations")}>Reservations</button>
            <button hidden={!employee.isFoodLocationEmployee} onClick={() => navigate("/inventory")}>Inventory</button>
            <button hidden={isEmployee} onClick={() => navigate("/discounts-taxes")}>
                Discounts & Taxes
            </button>
            <button hidden={isEmployee} onClick={() => navigate("/employees")}>
                Employees
            </button>

            <div className="move-to-bottom">
                <div className="nav-divider" />
                <button onClick={handleLogout}>Logout</button>
            </div>
        </nav>
    );
}
