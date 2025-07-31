import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';

const EditClient: React.FC = () => {
    const { id } = useParams<{ id: string }>();
    const navigate = useNavigate();

    const [name, setName] = useState('');
    const [address, setAddress] = useState('');
    const [status, setStatus] = useState<1 | 2>(1);
    const [loading, setLoading] = useState(true);
    const [saving, setSaving] = useState(false);
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        fetch(`/api/clients/${id}`)
            .then((res) =>
                res.json()
            )
            .then((data) => {
                setName(data.name);
                setAddress(data.address);
                setStatus(data.status);
                setLoading(false);
            });
    }, [id]);

    const handleSubmit = async (e: React.FormEvent) => {
        e.preventDefault();
        setSaving(true);
        setError(null);

        const response = await fetch(`/api/clients/${id}`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ name, address, status }),
        });

        if (!response.ok) {
            setError("Уже существует клиент с таким наименованием");
            setSaving(false);
        } else {
            navigate('/clients');
        }
    };

    const handleDelete = async () => {
        const response = await fetch(`/api/clients/${id}`, {
            method: 'DELETE'
        });

        if (!response.ok) {
            setError("Невозможно удалить, клиент уже используется в системе");
        } else {
            navigate('/clients');
        }
    }

    if (loading) return <div className="container mt-4">Загрузка...</div>;

    return (
        <div className="container mt-4">
            <h2>Редактировать клиента</h2>

            <form onSubmit={handleSubmit} className="mt-3" style={{ maxWidth: 500 }}>
                <div className="mb-3">
                    <label className="form-label">Имя</label>
                    <input type="text" className="form-control" required value={name} onChange={e => setName(e.target.value)} />
                </div>
                <div className="mb-3">
                    <label className="form-label">Адрес</label>
                    <input type="text" className="form-control" required value={address} onChange={e => setAddress(e.target.value)} />
                </div>
                <div className="mb-3">
                    <label className="form-label">Статус</label>
                    <select className="form-select" value={status} onChange={e => setStatus(Number(e.target.value) as 1 | 2)}>
                        <option value={1}>Рабочий</option>
                        <option value={2}>Архив</option>
                    </select>
                </div>

                {error && <div className="alert alert-danger">{error}</div>}

                <div className="d-flex gap-2">
                    <button type="submit" className="btn btn-primary" disabled={saving}>
                        {saving ? 'Сохранение...' : 'Сохранить'}
                    </button>
                    <button type="button" className='btn btn-danger' onClick={handleDelete}>Удалить</button>
                </div>
            </form>
        </div>
    );
};

export default EditClient;
