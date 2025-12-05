
import Navbar from "./components/NavBar/NavBar";
import ReservationPage from "./pages/Reservation/Reservation";
import "./App.css";

function App() {
  return (
    <div className="app-container">
      <Navbar />

      <div className="app-content">
        <ReservationPage />
      </div>
    </div>
  );
}

export default App;
