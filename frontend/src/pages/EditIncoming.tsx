import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import Incoming from '../models/Incoming';
import Resource from '../models/Resource';
import Unit from '../models/Unit';
import OrderItem from '../models/OrderItem';
import OrderItemsForm from '../components/OrderItemsForm';


const EditIncoming: React.FC = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const [incoming, setIncoming] = useState<Incoming>({ id: 0, number: '', date: '', items: [] });
    const [resources, setResources] = useState<Resource[]>([]);
    const [units, setUnits] = useState<Unit[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchData = async () => {
            const [incomingRes, resourcesRes, unitsRes] = await Promise.all([
                fetch(`/api/incomings/${id}`),
                fetch('/api/resources'),
                fetch('/api/units'),
            ]);

            const incomingData: Incoming = await incomingRes.json();
            const resourcesData: Resource[] = await resourcesRes.json();
            const unitsData: Unit[] = await unitsRes.json();

            setIncoming(incomingData);
            setResources(resourcesData);
            setUnits(unitsData);
            setLoading(false);
        };

        fetchData();
    }, [id]);

    const handleItemsChange = (newItems: OrderItem[]) => {
        setIncoming({ ...incoming, items: newItems });
    };

    const handleSave = async () => {

        const res = await fetch(`/api/incomings/${id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                ...incoming,
                items: incoming.items.map(item => ({
                    quantity: item.quantity, resourceId: item.resource.id, unitId: item.unit.id
                }))
            })
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

    const handleDelete = async () => {
        const response = await fetch(`/api/incomings/${id}`, { method: 'DELETE' });
        if (!response.ok) {
            setError("Недостаточно ресурсов на складе");
        }
        else {
            navigate('/incomings');
        }
    };

    if (loading) return <div className="container mt-4">Загрузка...</div>;

    return (
        <div className="container mt-4">
            <h2>Редактировать поступление</h2>

            <div className="mb-3">
                <label className="form-label">Номер</label>
                <input
                    type="text"
                    className="form-control"
                    value={incoming.number}
                    required
                    onChange={(e) => setIncoming({ ...incoming, number: e.target.value })}
                />
            </div>

            <div className="mb-3">
                <label className="form-label">Дата</label>
                <input
                    type="date"
                    className="form-control"
                    value={incoming.date}
                    required
                    onChange={(e) => setIncoming({ ...incoming, date: e.target.value })}
                />
            </div>

            <OrderItemsForm onItemsUpdate={handleItemsChange} resources={resources} units={units} items={incoming.items}></OrderItemsForm>

            {error && (
                <div className="alert alert-danger">{error}</div>
            )}

            <div className="mt-4">
                <button className="btn btn-primary me-2" onClick={handleSave}>
                    Сохранить
                </button>
                <button className="btn btn-danger me-2" onClick={handleDelete}>
                    Удалить
                </button>
                <button className="btn btn-outline-secondary" onClick={() => navigate('/incomings')}>
                    Отмена
                </button>
            </div>
        </div>
    );
};

export default EditIncoming;
