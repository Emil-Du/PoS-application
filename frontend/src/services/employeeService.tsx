const API_URL = "http://localhost:5041/api/Employee";

export interface UpdateEmployeeData {
  firstName: string;
  lastName: string;
  locationId: number;
  email: string;
  phoneNumber: string;
  status: string;
}

export async function getExactEmployee(employeeId: number ) {

  const res = await fetch(`${API_URL}?employeeId=${employeeId}`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json"
    },
    credentials: "include"
  });

  if (!res.ok) {
    throw new Error(await res.text());
  }

  return res.json();
}

export async function updateEmployee(employeeId: number, data: UpdateEmployeeData): Promise<void> {
  const res = await fetch(`${API_URL}/${employeeId}`, {
    method: "PATCH",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(data),
    credentials: "include"
  });

  if (!res.ok) {
    throw new Error(await res.text());
  }
}

export async function getEmployeesByLocation(locationId: number, page: number = 1, pageSize: number = 25, search?: string) {
  const params = new URLSearchParams({
    locationId: locationId.toString(),
    page: page.toString(),
    pageSize: pageSize.toString(),
  });

  if (search) {
    params.append("search", search);
  }

  const res = await fetch(`${API_URL}?${params.toString()}`, {
    method: "GET",
    headers: {
      "Content-Type": "application/json"
    },
    credentials: "include"
  });

  if (!res.ok) {
    throw new Error(await res.text());
  }

  return res.json();
}