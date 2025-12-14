import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

import { EmployeeProvider } from "./contexts/EmployeeContext"
import Login from "./pages/Login";
import Reservation from "./pages/Reservation";

function App() {
  return (
    <Router>
      <EmployeeProvider>
        <Routes>
          <Route path="/" element={<Login />} />

          <Route
            path="/reservation" element={ <Reservation />}
          />
        </Routes>
      </EmployeeProvider>
    </Router>
  );
}

export default App
