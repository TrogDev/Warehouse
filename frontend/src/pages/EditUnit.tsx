import React, { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';

const EditUnit: React.FC = () => {
  const { id } = useParams<{ id: string }>();
  const navigate = useNavigate();

  const [name, setName] = useState('');
  const [status, setState] = useState<1 | 2>(1);
  const [loading, setLoading] = useState(true);
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    fetch(`/api/units/${id}`)
      .then((res) => res.json())
      .then((data) => {
        setName(data.name);
        setState(data.status);
        setLoading(false);
      });
  }, [id]);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setSaving(true);
    setError(null);

    const response = await fetch(`/api/units/${id}`, {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify({ name, status }),
    });

    if (!response.ok) {
      setError("Уже существует единица измерения с таким наименованием");
      setSaving(false);
    } else {
      navigate('/units');
    }
  };

  const handleDelete = async () => {
    const response = await fetch(`/api/units/${id}`, {
      method: 'DELETE'
    });

    if (!response.ok) {
      setError("Невозможно удалить, единица уже используется в системе");
    } else {
      navigate('/units');
    }
  }

  if (loading) return <div className="container mt-4">Загрузка...</div>;

  return (
    <div className="container mt-4">
      <h2>Редактировать единицу измерения</h2>

      <form onSubmit={handleSubmit} className="mt-3" style={{ maxWidth: '500px' }}>
        <div className="mb-3">
          <label htmlFor="unitName" className="form-label">Наименование</label>
          <input
            type="text"
            className="form-control"
            id="unitName"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
          />
        </div>

        <div className="mb-3">
          <label htmlFor="unitState" className="form-label">Статус</label>
          <select
            className="form-select"
            id="unitState"
            value={status}
            onChange={(e) => setState(Number(e.target.value) as 1 | 2)}
          >
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

export default EditUnit;
