import { createContext, useContext, useState, useEffect } from "react";
import type { ReactNode } from "react";

interface Employee {
  employeeId: number;
  firstName: string;
  lastName: string;
  locationId: number;
  role: string;
}

interface EmployeeContextType {
  employee: Employee | null;
  setEmployee: (employee: Employee | null) => void;
}

const EmployeeContext = createContext<EmployeeContextType | undefined>(undefined);

export const EmployeeProvider = ({ children }: { children: ReactNode }) => {
  const [employee, setEmployee] = useState<Employee | null>(() => {
    // Restore from localStorage on page reload
    const saved = localStorage.getItem("employee");
    return saved ? JSON.parse(saved) : null;
  });

  // Persist changes to localStorage
  useEffect(() => {
    if (employee) localStorage.setItem("employee", JSON.stringify(employee));
    else localStorage.removeItem("employee");
  }, [employee]);

  return (
    <EmployeeContext.Provider value={{ employee, setEmployee }}>
      {children}
    </EmployeeContext.Provider>
  );
};

export const useEmployee = () => {
  const context = useContext(EmployeeContext);
  if (!context) throw new Error("useEmployee must be used within EmployeeProvider");
  return context;
};
