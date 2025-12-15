import "./NavBar.css";

export default function Navbar() {
    return (
        <nav className="navbar">
            <h2 className="nav-title">PoS-application</h2>
            <a href="#">Home</a>
            <a href="#">Reservations</a>
            <a href="#">Orders</a>
            <a href="#">Inventory</a>
            <a href="#">Employees</a>
        </nav>
    );
}
