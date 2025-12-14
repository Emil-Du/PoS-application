const API_URL = "http://localhost:5041/api/Provider";

export async function getProviders() {

  const res = await fetch(`${API_URL}`, {
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