import { useState, useEffect } from "react";
import { useEmployee } from "../contexts/EmployeeContext";
import { useNavigate } from "react-router-dom";
import { orderService } from "../services/orderService";
import Navbar from "../components/NavBar";
import "./Orders.css";

type OrderStatus = "Closed" | "Opened" | "Cancelled" | "Refunded";

interface OrderSummary {
  orderId: number;
  operatorId: number;
  status: OrderStatus;
  tip?: number;
  serviceCharge?: number;
  discount?: number;
  currency?: string;
  total?: number;
}

interface OrderItem {
  quantity: number;
  product: {
    productID: number;
    name: string;
    unitPrice: number;
    vatPercentage: number;
  };
  discount?: number;
}

interface OrderTotals {
  subtotal: number;
  tax: number;
  total: number;
}

export default function Orders() {
  const { employee } = useEmployee();
  const navigate = useNavigate();

  const [orderTotals, setOrderTotals] = useState<OrderTotals | null>(null);
  const [orders, setOrders] = useState<OrderSummary[]>([]);
  const [selectedOrder, setSelectedOrder] = useState<OrderSummary | null>(null);
  const [items, setItems] = useState<OrderItem[]>([]);
  const [statusFilter, setStatusFilter] = useState<OrderStatus | "All">("All");

  const filteredOrders =
    statusFilter === "All" ? orders : orders.filter((o) => o.status === statusFilter);

  useEffect(() => {
    if (!employee) {
      navigate("/");
      return;
    }

    const loadOrders = async () => {
      try {
        const ordersData = await orderService.getOrders(employee.locationId);
        const ordersWithTotals = await Promise.all(
          ordersData.map(async (order: OrderSummary) => {
            try {
              const totals = await orderService.getTotals(order.orderId);
              return { ...order, total: totals.total };
            } catch {
              return { ...order, total: 0 };
            }
          })
        );
        setOrders(ordersWithTotals);
      } catch (err) {
        console.error("Failed to load orders:", err);
      }
    };

    loadOrders();
  }, [employee, navigate]);

  const handleSelectOrder = async (order: OrderSummary) => {
  if (!employee) return;
  setItems([]);
  setOrderTotals(null);
  setSelectedOrder(order);

  try {
    const fetchedItems = await orderService.getOrderItems(order.orderId);
    const products = await orderService.getProducts(employee.locationId);
    const totals = await orderService.getTotals(order.orderId); // ✅

    const mappedItems: OrderItem[] = fetchedItems.map((item: any) => {
  const product = products.find((p: any) => p.productId === item.productId);

  return {
    quantity: item.quantity,
    discount: item.discount,
    product: {
      productID: item.productId,
      name: product?.name || `Product #${item.productId}`,
      unitPrice: Number(product?.unitPrice ?? 0),
      vatPercentage: Number(product?.vatPercent ?? 0), // ✅ FIX
    },
  };
});


    setSelectedOrder(order);
    setItems(mappedItems);
    setOrderTotals(totals); // ✅
  } catch (err) {
    console.error("Failed to load order details:", err);
    setItems([]);
    setOrderTotals(null);
  }
};


  const refundOrder = async () => {
  if (!selectedOrder) return;

  try {
    await orderService.updateOrder(selectedOrder.orderId, {
      status: "Refunded",
    });

    setSelectedOrder((prev) =>
      prev ? { ...prev, status: "Refunded" } : prev
    );

    setOrders((prev) =>
      prev.map((o) =>
        o.orderId === selectedOrder.orderId
          ? { ...o, status: "Refunded" }
          : o
      )
    );

  } catch (err) {
    console.error("Failed to refund order:", err);
    alert("Failed to refund order");
  }
};

  return (
    <div className="layout">
      <Navbar />

      <div className="orders-module">
        <div className="order-selection-tab">
          <p>ORDERS</p>
        </div>

        <div className="input-columns">
          <div className="column orders-list-column">
            <div className="orders-filter">
              {["All", "Closed", "Opened", "Cancelled", "Refunded"].map((status) => (
                <button
                  key={status}
                  className={statusFilter === status ? "selected-button" : ""}
                  onClick={() => setStatusFilter(status as any)}
                >
                  {status}
                </button>
              ))}
            </div>

            <div className="orders-table-header">
              <span>ID</span>
              <span>Total</span>
              <span>Status</span>
            </div>

            <div className="scroll-container">
              {filteredOrders.map((order) => (
                <button
                  key={order.orderId}
                  className={`order-row ${
                    selectedOrder?.orderId === order.orderId ? "selected-button" : ""
                  }`}
                  onClick={() => handleSelectOrder(order)}
                >
                  <span>#{order.orderId}</span>
                  <span>{order.total?.toFixed(2) ?? "0.00"}€</span>
                  <span>{order.status}</span>
                </button>
              ))}
            </div>
          </div>

          <div className="column order-details-column">
            <p>ORDER DETAILS</p>

            {!selectedOrder ? (
              <div className="empty-details">Select an order to view details</div>
            ) : (
              <>
                <div className="details">
                  <p>
                    ORDER #{selectedOrder.orderId} - status: {selectedOrder.status.toLowerCase()}
                  </p>

                  {items.map((item, idx) => (
                    <div key={idx} className="details-data">
                      <div>{item.product.name} x {item.quantity}</div>
                      <div>{(item.product.unitPrice * item.quantity).toFixed(2)} €</div>
                    </div>
                  ))}

                  {orderTotals && (
                    <div className="order-summary">
                      <div className="summary-row">
                        <span>Subtotal</span>
                        <span>{orderTotals.subtotal.toFixed(2)} €</span>
                      </div>

                      <div className="summary-row">
                        <span>Tax</span>
                        <span>{orderTotals.tax.toFixed(2)} €</span>
                      </div>

                      <div className="summary-row summary-total">
                        <strong>Total</strong>
                        <strong>{orderTotals.total.toFixed(2)} €</strong>
                      </div>
                    </div>
                  )}
                </div>

                {selectedOrder.status === "Closed" && (
                  <button className="refund-button" onClick={refundOrder}>
                    Refund
                  </button>
                )}
              </>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}
