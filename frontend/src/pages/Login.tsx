import './Login.css'
import { useState } from "react";
import { FaEnvelope, FaLock } from "react-icons/fa";
import { login } from '../services/authService'
import { useEmployee } from "../contexts/EmployeeContext";
import { useNavigate } from "react-router-dom";
import { getRoleIdByEmployeeId, getRoleById } from '../services/roleService';
import { getServices } from "../services/serviceService";

function Login() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState<string | null> (null);
  const { setEmployee } = useEmployee();
  const navigate = useNavigate();

  const isDisabled = !email.includes("@") || !password;

  const submitLoginDetails = async (e: React.FormEvent) => {
    e.preventDefault();
    setError(null);

    try {
      const employeeData = await login(email, password);

      const roleId = await getRoleIdByEmployeeId(employeeData.employeeId);

      const role = (await getRoleById(roleId)).name;

      const isFoodLocation = (await getServices(employeeData.locationId)).length === 0

      const employeeWithRoleAndLocationType = {
        ...employeeData,
        role: role,
        isFoodLocationEmployee: isFoodLocation
      };

      setEmployee(employeeWithRoleAndLocationType);

      isFoodLocation ? navigate("/home") : navigate("/reservations");
    } 
    catch (err: any) {
      setError(err.message);
    }
  };

  return (
    <>
      <main> 
          <h1>PoS Application</h1>

          <div className="center-box">
            <div className='login-text'>LOGIN / AUTHENTICANTION</div>

            <form onSubmit={submitLoginDetails}>
              <label>EMAIL ADDRESS
                <div className='input-with-icon'>
                  <FaEnvelope className="icon" />
                  <input type="text" required placeholder='Enter email...' value={email} onChange={(e) => setEmail(e.target.value)}/>
                </div>
              </label>

              <label>PASSWORD
                <div className='input-with-icon'>
                  <FaLock className="icon" />
                  <input type="password" required placeholder='Enter password...' value={password} onChange={(e) => setPassword(e.target.value)}/>
                </div>
              </label>

              <button type="submit" disabled={isDisabled}>Login</button>
            </form>
          </div>
      </main>
    </>
  )
}

export default Login;
