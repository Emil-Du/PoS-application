const API_URL = "http://localhost:5041/api/customer";

export async function getCustomer(name: string, phoneNumber: string) {
  const res = await fetch(`${API_URL}?name=${name}?phoneNumber=${phoneNumber}`, {
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

export async function registerCustomer(name: string, phoneNumber: string) {
  const res = await fetch(`${API_URL}`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify({ name, phoneNumber }),
    credentials: "include"
  });

  if (!res.ok) {
    throw new Error(await res.text());
  }

  return res.json();
}