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

export async function GetReservations(params: {serviceId?: number, locationId?: number, providerId?: number, customerId?: number, from?: number, to?: number, status?: string} ) {

  const query = new URLSearchParams();

  Object.entries(params).forEach(([key, value]) => {
    if (value !== undefined && value !== null) {
      query.append(key, value.toString());
    }
  });

  const res = await fetch(`${API_URL}/Reservations?${query.toString()}`, {
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