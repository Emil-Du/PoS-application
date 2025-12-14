const API_URL = "http://localhost:5041/api/Auth";

export async function login(email: string, password: string) {
  const res = await fetch(`${API_URL}/LoginEmployee`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify({ email, password }),
    credentials: "include" //no accessToken yet
  });

  if (!res.ok) {
    throw new Error(await res.text());
  }

  return res.json();
}