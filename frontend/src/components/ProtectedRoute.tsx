import { Navigate } from "react-router-dom";
import type { JSX } from "react/jsx-runtime";

// naudoti App.tsx kad neprieitu prie kitu puslapiu be accessToken (neprisijunge naudotojai)

export default function ProtectedRoute({ children }: { children: JSX.Element }) {
  const token = document.cookie.includes("accessToken");

  return token ? children : <Navigate to="/" replace />;
}
