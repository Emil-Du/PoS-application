import { BrowserRouter as Router, Routes, Route } from "react-router-dom";

import Login from "./pages/Login";
import Home from "./pages/Home";
import Reservation from "./pages/Reservation";
import NavBar from "./components/NavBar";


function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/Home" element={<Home />} />
        <Route path="/Reservation" element={<Reservation />} />
        <Route path="/NavBar" element={<NavBar />} />
      </Routes>
    </Router>
  );
}

export default App
