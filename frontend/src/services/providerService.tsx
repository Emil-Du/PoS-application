const API_URL = "http://localhost:5041/api/Provider";

export async function getProviders(locationId: number) {

  const res = await fetch(`${API_URL}?locationid=${locationId}`, {
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