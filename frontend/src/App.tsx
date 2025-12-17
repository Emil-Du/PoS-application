import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

import { EmployeeProvider } from "./contexts/EmployeeContext"
import Login from "./pages/Login";
import Reservation from "./pages/Reservation";
import Home from "./pages/Home";
import Orders from "./pages/Orders";
import Employees from "./pages/Employees";

function App() {
  return (
    <Router>
      <EmployeeProvider>
        <Routes>
          <Route path="/" element={<Login />} />

          <Route
            path="/reservations" element={<Reservation />}

          />
          <Route
            path="/home" element={<Home />}
          />
          <Route
            path="/orders" element={ <Orders />}
          />
          <Route
            path="/employees" element={<Employees />}
          />
        </Routes>
      </EmployeeProvider>
    </Router>
  );
}

export default App
