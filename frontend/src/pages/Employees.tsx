import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useEmployee } from "../contexts/EmployeeContext";
import { getEmployeesByLocation } from "../services/employeeService";
import { registerEmployee, type RegisterEmployeeData } from "../services/authService";
import Navbar from "../components/NavBar";
import "./Employees.css";

interface EmployeeData {
  employeeId: number;
  firstName: string;
  lastName: string;
  locationId: number;
  phoneNumber: string;
  email: string;
  status: string;
}

interface EmployeesResponse {
  data: EmployeeData[];
  page: number;
  pageSize: number;
  total: number;
}

export default function Employees() {
  const { employee } = useEmployee();
  const navigate = useNavigate();
  const [employees, setEmployees] = useState<EmployeeData[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [searchTerm, setSearchTerm] = useState<string>("");
  const [page, setPage] = useState<number>(1);
  const [totalPages, setTotalPages] = useState<number>(1);
  const [totalCount, setTotalCount] = useState<number>(0);

  // New states for employee management
  const [selectedEmployee, setSelectedEmployee] = useState<EmployeeData | null>(null);
  const [isAddingNew, setIsAddingNew] = useState<boolean>(false);
  const [submitting, setSubmitting] = useState<boolean>(false);

  // Hardcoded roles: 1 = Employee, 2 = Manager
  const roles = [
    { roleId: 1, name: "Employee" },
    { roleId: 2, name: "Manager" }
  ];

  // Form states
  const [formData, setFormData] = useState({
    firstName: "",
    lastName: "",
    email: "",
    phoneNumber: "",
    password: "",
    roleId: 1  // Default to Employee role
  });

  // Form validation errors
  const [errors, setErrors] = useState({
    firstName: "",
    lastName: "",
    email: "",
    phoneNumber: "",
    password: "",
    roleId: ""
  });

  useEffect(() => {
    if (!employee) {
      navigate("/");
      return;
    }

    const fetchData = async () => {
      try {
        setLoading(true);

        const response = await getEmployeesByLocation(employee.locationId, page, 25, searchTerm || undefined);

        setEmployees(response.data || []);
        const calculatedTotalPages = Math.ceil(response.total / response.pageSize) || 1;
        setTotalPages(calculatedTotalPages);
        setTotalCount(response.total || 0);
      } catch (err) {
        console.error("Failed to fetch data:", err);
        setEmployees([]);
        setTotalPages(1);
        setTotalCount(0);
        alert("Failed to load data. Please try again.");
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, [employee, navigate, page, searchTerm]);

  const handleSearch = (value: string) => {
    setSearchTerm(value);
    setPage(1);
  };

  const handlePreviousPage = () => {
    if (page > 1) setPage(page - 1);
  };

  const handleNextPage = () => {
    if (page < totalPages) setPage(page + 1);
  };

  const handleAddNewClick = () => {
    setIsAddingNew(true);
    setSelectedEmployee(null);
    setFormData({
      firstName: "",
      lastName: "",
      email: "",
      phoneNumber: "",
      password: "",
      roleId: 1  // Default to Employee role
    });
    setErrors({
      firstName: "",
      lastName: "",
      email: "",
      phoneNumber: "",
      password: "",
      roleId: ""
    });
  };

  const handleEmployeeClick = (emp: EmployeeData) => {
    setSelectedEmployee(emp);
    setIsAddingNew(false);
  };

  const handleCancel = () => {
    setIsAddingNew(false);
    setSelectedEmployee(null);
    setFormData({
      firstName: "",
      lastName: "",
      email: "",
      phoneNumber: "",
      password: "",
      roleId: 1  // Default to Employee role
    });
    setErrors({
      firstName: "",
      lastName: "",
      email: "",
      phoneNumber: "",
      password: "",
      roleId: ""
    });
  };

  const validateForm = (): boolean => {
    const newErrors = {
      firstName: "",
      lastName: "",
      email: "",
      phoneNumber: "",
      password: "",
      roleId: ""
    };

    let isValid = true;

    if (!formData.firstName.trim()) {
      newErrors.firstName = "First name is required";
      isValid = false;
    }

    if (!formData.lastName.trim()) {
      newErrors.lastName = "Last name is required";
      isValid = false;
    }

    if (!formData.email.trim()) {
      newErrors.email = "Email is required";
      isValid = false;
    } else if (!/^[^\s@]+@[^\s@]+\.[^\s@]+$/.test(formData.email)) {
      newErrors.email = "Invalid email format";
      isValid = false;
    }

    if (!formData.phoneNumber.trim()) {
      newErrors.phoneNumber = "Phone number is required";
      isValid = false;
    }

    if (!formData.password.trim()) {
      newErrors.password = "Password is required";
      isValid = false;
    } else if (formData.password.length < 6) {
      newErrors.password = "Password must be at least 6 characters";
      isValid = false;
    }

    if (formData.roleId === 0) {
      newErrors.roleId = "Please select a role";
      isValid = false;
    }

    setErrors(newErrors);
    return isValid;
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    if (!validateForm() || !employee) return;

    try {
      setSubmitting(true);

      const registerData: RegisterEmployeeData = {
        firstName: formData.firstName,
        lastName: formData.lastName,
        email: formData.email,
        phoneNumber: formData.phoneNumber,
        password: formData.password,
        roleId: formData.roleId,
        locationId: employee.locationId
      };

      const newEmployee = await registerEmployee(registerData);

      // Refresh employee list
      const response = await getEmployeesByLocation(employee.locationId, page, 25, searchTerm || undefined);
      setEmployees(response.data || []);
      const calculatedTotalPages = Math.ceil(response.total / response.pageSize) || 1;
      setTotalPages(calculatedTotalPages);
      setTotalCount(response.total || 0);

      // Show the newly created employee in the card
      const createdEmployee: EmployeeData = {
        employeeId: newEmployee.employeeId,
        firstName: newEmployee.firstName,
        lastName: newEmployee.lastName,
        locationId: newEmployee.locationId,
        email: newEmployee.email,
        phoneNumber: newEmployee.phoneNumber,
        status: newEmployee.status
      };

      setSelectedEmployee(createdEmployee);
      setIsAddingNew(false);

      // Reset form
      setFormData({
        firstName: "",
        lastName: "",
        email: "",
        phoneNumber: "",
        password: "",
        roleId: 1  // Default to Employee role
      });
    } catch (err: any) {
      console.error("Failed to create employee:", err);
      alert(err.message || "Failed to create employee. Please try again.");
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <div className="layout">
      <Navbar />

      <div className="module">
        <div className="module-header">
          <p>Employees at Location {employee?.locationId}</p>
        </div>

        <div className="employees-container">
          <div className="employees-left">
            <div className="search-section">
              <input
                type="text"
                className="search-input"
                placeholder="Search by name, email, or phone..."
                value={searchTerm}
                onChange={(e) => handleSearch(e.target.value)}
              />
              <button className="add-employee-button" onClick={handleAddNewClick}>
                + Add New Employee
              </button>
            </div>

            {loading ? (
              <div className="empty-state">Loading employees...</div>
            ) : !employees || employees.length === 0 ? (
              <div className="empty-state">
                {searchTerm
                  ? "No employees found matching your search."
                  : "No employees at this location."}
              </div>
            ) : (
              <>
                <div className="employees-table">
                  <div className="table-header">
                    <div className="col-id">ID</div>
                    <div className="col-name">Name</div>
                    <div className="col-email">Email</div>
                    <div className="col-phone">Phone</div>
                    <div className="col-status">Status</div>
                  </div>

                  {employees.map((emp) => (
                    <div
                      key={emp.employeeId}
                      className={`table-row ${selectedEmployee?.employeeId === emp.employeeId ? "selected" : ""}`}
                      onClick={() => handleEmployeeClick(emp)}
                    >
                      <div className="col-id">{emp.employeeId}</div>
                      <div className="col-name">
                        {emp.firstName} {emp.lastName}
                      </div>
                      <div className="col-email">{emp.email}</div>
                      <div className="col-phone">{emp.phoneNumber}</div>
                      <div className="col-status">
                        <span className={`status-badge ${emp.status.toLowerCase()}`}>
                          {emp.status}
                        </span>
                      </div>
                    </div>
                  ))}
                </div>

                <div className="pagination">
                  <button
                    onClick={handlePreviousPage}
                    disabled={page === 1}
                    className="pagination-button"
                  >
                    Previous
                  </button>
                  <span className="pagination-info">
                    Page {page} of {totalPages} ({totalCount} total employees)
                  </span>
                  <button
                    onClick={handleNextPage}
                    disabled={page === totalPages}
                    className="pagination-button"
                  >
                    Next
                  </button>
                </div>
              </>
            )}
          </div>

          <div className="employees-right">
            {isAddingNew ? (
              <div className="employee-card">
                <h3>Add New Employee</h3>
                <form onSubmit={handleSubmit}>
                  <div className="form-group">
                    <label>First Name *</label>
                    <input
                      type="text"
                      value={formData.firstName}
                      onChange={(e) => setFormData({ ...formData, firstName: e.target.value })}
                      className={errors.firstName ? "error" : ""}
                    />
                    {errors.firstName && <span className="error-message">{errors.firstName}</span>}
                  </div>

                  <div className="form-group">
                    <label>Last Name *</label>
                    <input
                      type="text"
                      value={formData.lastName}
                      onChange={(e) => setFormData({ ...formData, lastName: e.target.value })}
                      className={errors.lastName ? "error" : ""}
                    />
                    {errors.lastName && <span className="error-message">{errors.lastName}</span>}
                  </div>

                  <div className="form-group">
                    <label>Email *</label>
                    <input
                      type="email"
                      value={formData.email}
                      onChange={(e) => setFormData({ ...formData, email: e.target.value })}
                      className={errors.email ? "error" : ""}
                    />
                    {errors.email && <span className="error-message">{errors.email}</span>}
                  </div>

                  <div className="form-group">
                    <label>Phone Number *</label>
                    <input
                      type="tel"
                      value={formData.phoneNumber}
                      onChange={(e) => setFormData({ ...formData, phoneNumber: e.target.value })}
                      className={errors.phoneNumber ? "error" : ""}
                    />
                    {errors.phoneNumber && <span className="error-message">{errors.phoneNumber}</span>}
                  </div>

                  <div className="form-group">
                    <label>Password *</label>
                    <input
                      type="password"
                      value={formData.password}
                      onChange={(e) => setFormData({ ...formData, password: e.target.value })}
                      className={errors.password ? "error" : ""}
                    />
                    {errors.password && <span className="error-message">{errors.password}</span>}
                  </div>

                  <div className="form-group">
                    <label>Role *</label>
                    <select
                      value={formData.roleId}
                      onChange={(e) => setFormData({ ...formData, roleId: Number(e.target.value) })}
                      className={errors.roleId ? "error" : ""}
                    >
                      {roles.map((role) => (
                        <option key={role.roleId} value={role.roleId}>
                          {role.name}
                        </option>
                      ))}
                    </select>
                    {errors.roleId && <span className="error-message">{errors.roleId}</span>}
                  </div>

                  <div className="form-group">
                    <label>Location</label>
                    <input
                      type="text"
                      value={`Location ${employee?.locationId}`}
                      disabled
                      className="disabled-input"
                    />
                  </div>

                  <div className="form-actions">
                    <button type="button" onClick={handleCancel} className="cancel-button">
                      Cancel
                    </button>
                    <button type="submit" className="save-button" disabled={submitting}>
                      {submitting ? "Saving..." : "Save Employee"}
                    </button>
                  </div>
                </form>
              </div>
            ) : selectedEmployee ? (
              <div className="employee-card">
                <h3>Employee Details</h3>
                <div className="employee-info">
                  <div className="info-row">
                    <span className="info-label">ID:</span>
                    <span className="info-value">{selectedEmployee.employeeId}</span>
                  </div>
                  <div className="info-row">
                    <span className="info-label">Name:</span>
                    <span className="info-value">{selectedEmployee.firstName} {selectedEmployee.lastName}</span>
                  </div>
                  <div className="info-row">
                    <span className="info-label">Email:</span>
                    <span className="info-value">{selectedEmployee.email}</span>
                  </div>
                  <div className="info-row">
                    <span className="info-label">Phone:</span>
                    <span className="info-value">{selectedEmployee.phoneNumber}</span>
                  </div>
                  <div className="info-row">
                    <span className="info-label">Status:</span>
                    <span className={`status-badge ${selectedEmployee.status.toLowerCase()}`}>
                      {selectedEmployee.status}
                    </span>
                  </div>
                </div>
              </div>
            ) : (
              <div className="employee-card empty">
                <p>Select an employee to view details or click "Add New Employee" to create one.</p>
              </div>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}
