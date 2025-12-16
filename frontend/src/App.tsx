import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

import { EmployeeProvider } from "./contexts/EmployeeContext"
import Login from "./pages/Login";
import Reservation from "./pages/Reservation";
import Home from "./pages/Home";

function App() {
  return (
    <Router>
      <EmployeeProvider>
        <Routes>
          <Route path="/" element={<Login />} />

          <Route
            path="/reservation" element={<Reservation />}

          />
          <Route
            path="/home" element={<Home />}
          />
        </Routes>
      </EmployeeProvider>
    </Router>
  );
}

export default App
