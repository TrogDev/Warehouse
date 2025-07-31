import React, { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import Incoming from '../models/Incoming';
import Resource from '../models/Resource';
import Unit from '../models/Unit';
import OrderItem from '../models/OrderItem';
import OrderItemsForm from '../components/OrderItemsForm';


const AddIncoming: React.FC = () => {
    const navigate = useNavigate();

    const [incoming, setIncoming] = useState<Incoming>({ id: 0, number: '', date: new Date().toISOString().substring(0, 10), items: [] });
    const [resources, setResources] = useState<Resource[]>([]);
    const [units, setUnits] = useState<Unit[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchData = async () => {
            const [resourcesRes, unitsRes] = await Promise.all([
                fetch('/api/resources'),
                fetch('/api/units'),
            ]);

            const resourcesData = await resourcesRes.json();
            const unitsData = await unitsRes.json();

            setResources(resourcesData);
            setUnits(unitsData);
            setLoading(false);
        };

        fetchData();
    }, []);

    const handleItemsChange = (newItems: OrderItem[]) => {
        setIncoming({ ...incoming, items: newItems });
    };

    const handleCreate = async () => {
        const res = await fetch(`/api/incomings`, {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                ...incoming,
                items: incoming.items.map(item => ({
                    quantity: item.quantity, resourceId: item.resource.id, unitId: item.unit.id
                }))
            }),
        });

        if (!res.ok) {
            const errorData: Record<string, string[]> = await res.json();
            setError(Object.entries(errorData)
                .map(([key, messages]) => messages.join(", "))
                .join(", "));
        } else {
            navigate('/incomings');
        }
    };

    if (loading) return <div className="container mt-4">Загрузка...</div>;

    return (
        <div className="container mt-4">
            <h2>Создание поступление</h2>

            <div className="mb-3">
                <label className="form-label">Номер</label>
                <input
                    type="text"
                    className="form-control"
                    required
                    value={incoming.number}
                    onChange={(e) => setIncoming({ ...incoming, number: e.target.value })}
                />
            </div>

            <div className="mb-3">
                <label className="form-label">Дата</label>
                <input
                    type="date"
                    className="form-control"
                    required
                    value={incoming.date}
                    onChange={(e) => setIncoming({ ...incoming, date: e.target.value })}
                />
            </div>

            <OrderItemsForm onItemsUpdate={handleItemsChange} resources={resources} units={units} items={incoming.items}></OrderItemsForm>

            {error && (
                <div className="alert alert-danger">{error}</div>
            )}

            <div className="mt-4">
                <button className="btn btn-primary me-2" onClick={handleCreate}>
                    Создать
                </button>
                <button className="btn btn-outline-secondary" onClick={() => navigate('/incomings')}>
                    Отмена
                </button>
            </div>
        </div>
    );
};

export default AddIncoming;
