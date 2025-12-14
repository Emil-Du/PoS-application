const API_URL = "http://localhost:5041/api";

export async function CreateReservation(serviceId: number, locationId: number, providerId: number, customerId: number, appointmentTime: number ) {

  const res = await fetch(`${API_URL}/Reservations`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify({ serviceId, locationId, providerId, customerId, appointmentTime }),
    credentials: "include"
  });

  if (!res.ok) {
    throw new Error(await res.text());
  }

  return res.json();
}