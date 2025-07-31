import React from 'react';
import OrderItem from '../models/OrderItem';
import Resource from '../models/Resource';
import Unit from '../models/Unit';

interface OrderItemsFormProps {
    onItemsUpdate: (items: OrderItem[]) => void;
    items: OrderItem[];
    resources: Resource[];
    units: Unit[];
}

const OrderItemsForm: React.FC<OrderItemsFormProps> = ({ onItemsUpdate, items = [], resources = [], units = [] }) => {
    const [formItems, setFormItems] = React.useState<OrderItem[]>(items);

    const handleItemResourceChange = (index: number, value: any) => {
        const resource = resources.find(r => r.id == value)!;
        formItems[index] = { ...formItems[index], resource: resource };
        handleItemsChange(formItems);
    };

    const handleItemUnitChange = (index: number, value: any) => {
        const unit = units.find(u => u.id == value)!;
        formItems[index] = { ...formItems[index], unit: unit };
        handleItemsChange(formItems);
    };

    const handleItemQuantityChange = (index: number, value: any) => {
        const quantity = parseInt(value);
        formItems[index] = { ...formItems[index], quantity: quantity };
        handleItemsChange(formItems);
    };

    const handleAddItem = () => {
        handleItemsChange(
            [...formItems, { id: 0, resource: resources[0], unit: units[0], quantity: 1 }]
        );
    };

    const handleRemoveItem = (index: number) => {
        const updatedItems = formItems.filter((_, i) => i !== index);
        handleItemsChange(updatedItems);
    };

    const handleItemsChange = (newItems: OrderItem[]) => {
        setFormItems(newItems);
        onItemsUpdate(newItems);
    }

    return (
        <div>
            <h5>Товары</h5>
            {formItems.map((item, index) => (
                <div className="row mb-2" key={index}>
                    <div className="col-md-4">
                        <select
                            className="form-select"
                            value={item.resource.id}
                            onChange={(e) => handleItemResourceChange(index, e.target.value)}
                        >
                            {resources.filter(e => e.status === 1 || e.id === item.resource.id).map((r) => (
                                <option key={r.id} value={r.id}>{r.name}</option>
                            ))}
                        </select>
                    </div>
                    <div className="col-md-3">
                        <select
                            className="form-select"
                            value={item.unit.id}
                            onChange={(e) => handleItemUnitChange(index, e.target.value)}
                        >
                            {units.filter(e => e.status === 1 || e.id === item.unit.id).map((u) => (
                                <option key={u.id} value={u.id}>{u.name}</option>
                            ))}
                        </select>
                    </div>
                    <div className="col-md-3">
                        <input
                            type="number"
                            className="form-control"
                            value={item.quantity}
                            onChange={(e) => handleItemQuantityChange(index, e.target.value)}
                        />
                    </div>
                    <div className="col-md-2">
                        <button
                            type="button"
                            className="btn btn-danger w-100"
                            onClick={() => handleRemoveItem(index)}
                        >
                            Удалить
                        </button>
                    </div>
                </div>
            ))}

            <button type="button" className="btn btn-secondary mt-2" onClick={handleAddItem}>
                Добавить товар
            </button>
        </div>
    );
};

export default OrderItemsForm;
