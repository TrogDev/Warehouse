import Resource from "./Resource";
import Unit from "./Unit";

export default interface OrderItem {
  id: number;
  resource: Resource;
  unit: Unit;
  quantity: number;
}
