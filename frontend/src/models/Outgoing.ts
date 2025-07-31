import Client from "./Client";
import OrderItem from "./OrderItem";

export default interface Incoming {
  id: number;
  number: string;
  date: string;
  isSigned: boolean;
  client: Client;
  items: OrderItem[];
}
