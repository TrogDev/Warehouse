import OrderItem from "./OrderItem";

export default interface Incoming {
  id: number;
  number: string;
  date: string;
  items: OrderItem[];
}
