import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const AddResource: React.FC = () => {
  const [name, setName] = useState('');
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setSaving(true);
    setError(null);

    const response = await fetch('/api/resources', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({ name })
    });

    if (!response.ok) {
      setError("Уже существует ресурс с таким наименованием");
      setSaving(false);
    } else {
      navigate('/resources');
    }
  };

  return (
    <div className="container mt-4">
      <h2>Добавить ресурс</h2>

      <form onSubmit={handleSubmit} className="mt-3" style={{ maxWidth: '500px' }}>
        <div className="mb-3">
          <label htmlFor="resourceName" className="form-label">Наименование</label>
          <input
            type="text"
            className="form-control"
            id="resourceName"
            value={name}
            onChange={(e) => setName(e.target.value)}
            required
          />
        </div>

        {error && (
          <div className="alert alert-danger">{error}</div>
        )}

        <button type="submit" className="btn btn-primary" disabled={saving}>
          {saving ? 'Сохранение...' : 'Сохранить'}
        </button>
      </form>
    </div>
  );
};

export default AddResource;
