const API_URL = "http://localhost:5041/api/v1/Role";

export interface Role {
  roleId: number;
  name: string;
}

export async function getRoles(): Promise<Role[]> {
  const res = await fetch(API_URL, {
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

export async function getRoleIdByEmployeeId(employeeId: number) {

  const res = await fetch(`${API_URL}/RoleIdByEmployeeId?employeeId=${employeeId}`, {
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

export async function getRoleById(roleId: number ) {

  const res = await fetch(`${API_URL}/${roleId}`, {
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