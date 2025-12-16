import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";
import { useEmployee } from "../contexts/EmployeeContext";
import { getEmployeesByLocation } from "../services/employeeService";
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

  useEffect(() => {
    if (!employee) {
      navigate("/");
      return;
    }

    const fetchEmployees = async () => {
      try {
        setLoading(true);
        const response: EmployeesResponse = await getEmployeesByLocation(
          employee.locationId,
          page,
          25,
          searchTerm || undefined
        );
        setEmployees(response.data || []);
        const calculatedTotalPages = Math.ceil(response.total / response.pageSize) || 1;
        setTotalPages(calculatedTotalPages);
        setTotalCount(response.total || 0);
      } catch (err) {
        console.error("Failed to fetch employees:", err);
        setEmployees([]);
        setTotalPages(1);
        setTotalCount(0);
        alert("Failed to load employees. Please try again.");
      } finally {
        setLoading(false);
      }
    };

    fetchEmployees();
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

  return (
    <div className="layout">
      <Navbar />

      <div className="module">
        <div className="module-header">
          <p>Employees at Location {employee?.locationId}</p>
        </div>

        <div className="employees-content">
          <div className="search-bar">
            <input
              type="text"
              placeholder="Search by name, email, or phone..."
              value={searchTerm}
              onChange={(e) => handleSearch(e.target.value)}
            />
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
                  <div key={emp.employeeId} className="table-row">
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
      </div>
    </div>
  );
}
