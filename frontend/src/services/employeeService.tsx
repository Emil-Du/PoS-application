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