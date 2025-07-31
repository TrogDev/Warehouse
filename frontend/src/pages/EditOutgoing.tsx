import React, { useEffect, useState } from 'react';
import { useParams, useNavigate } from 'react-router-dom';
import Outgoing from '../models/Outgoing';
import Resource from '../models/Resource';
import Unit from '../models/Unit';
import OrderItem from '../models/OrderItem';
import OrderItemsForm from '../components/OrderItemsForm';
import Client from '../models/Client';


const EditOutgoing: React.FC = () => {
    const { id } = useParams();
    const navigate = useNavigate();

    const [outgoing, setOutgoing] = useState<Outgoing>(null!);
    const [resources, setResources] = useState<Resource[]>([]);
    const [units, setUnits] = useState<Unit[]>([]);
    const [clients, setClients] = useState<Client[]>([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        const fetchData = async () => {
            const [outgoingRes, resourcesRes, unitsRes, clientsRes] = await Promise.all([
                fetch(`/api/outgoings/${id}`),
                fetch('/api/resources'),
                fetch('/api/units'),
                fetch('/api/clients')
            ]);

            const outgoingData = await outgoingRes.json();
            const resourcesData = await resourcesRes.json();
            const unitsData = await unitsRes.json();
            const clientsData = await clientsRes.json();

            setOutgoing(outgoingData);
            setResources(resourcesData);
            setUnits(unitsData);
            setClients(clientsData);
            setLoading(false);
        };

        fetchData();
    }, [id]);

    const handleItemsChange = (newItems: OrderItem[]) => {
        setOutgoing({ ...outgoing, items: newItems });
    };

    const handleCreate = async () => {
        const res = await fetch(`/api/outgoings/${id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({
                ...outgoing,
                clientId: outgoing.client.id,
                items: outgoing.items.map(item => ({
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
            navigate('/outgoings');
        }
    };

    const handleDelete = async () => {
        await fetch(`/api/outgoings/${id}`, { method: 'DELETE' });
        navigate('/outgoings');
    }

    if (loading) return <div className="container mt-4">Загрузка...</div>;

    return (
        <div className="container mt-4">
            <h2>Редактирование отгрузки</h2>

            <div className="mb-3">
                <label className="form-label">Номер</label>
                <input
                    type="text"
                    className="form-control"
                    required
                    value={outgoing.number}
                    onChange={(e) => setOutgoing({ ...outgoing, number: e.target.value })}
                />
            </div>

            <div className="mb-3">
                <label className="form-label">Дата</label>
                <input
                    type="date"
                    className="form-control"
                    required
                    value={outgoing.date}
                    onChange={(e) => setOutgoing({ ...outgoing, date: e.target.value })}
                />
            </div>

            <div className="mb-3">
                <label className="form-label">Клиент</label>
                <select
                    className="form-select"
                    value={outgoing.client.id}
                    onChange={(e) => setOutgoing({ ...outgoing, client: clients.find(c => c.id.toString() === e.target.value)! })}
                >
                    {clients.filter(e => e.status === 1 || e.id === outgoing.client.id).map((u) => (
                        <option key={u.id} value={u.id}>{u.name}</option>
                    ))}
                </select>
            </div>

            <div className="mb-3 d-flex gap-2">
                <label className="form-label">Подпись</label>
                <input
                    type="checkbox"
                    className="form-check-input"
                    checked={outgoing.isSigned}
                    onChange={() => setOutgoing({ ...outgoing, isSigned: !outgoing.isSigned })}
                />
            </div>

            <OrderItemsForm onItemsUpdate={handleItemsChange} resources={resources} units={units} items={outgoing.items}></OrderItemsForm>

            {error && (
                <div className="alert alert-danger">{error}</div>
            )}

            <div className="mt-4">
                <button className="btn btn-primary me-2" onClick={handleCreate}>
                    Сохранить
                </button>
                <button className="btn btn-danger me-2" onClick={handleDelete}>
                    Удалить
                </button>
                <button className="btn btn-outline-secondary" onClick={() => navigate('/outgoings')}>
                    Отмена
                </button>
            </div>
        </div>
    );
};

export default EditOutgoing;
