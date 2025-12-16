const API_URL = "http://localhost:5041/api/Employee";

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