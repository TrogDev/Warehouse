import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';

const AddClient: React.FC = () => {
  const [name, setName] = useState('');
  const [address, setAddress] = useState('');
  const [saving, setSaving] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setSaving(true);
    setError(null);

    const response = await fetch('/api/clients', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify({ name, address }),
    });

    if (!response.ok) {
      setError("Уже существует клиент с таким наименованием");
      setSaving(false);
    } else {
      navigate('/clients');
    }
  };

  return (
    <div className="container mt-4">
      <h2>Добавить клиента</h2>

      <form onSubmit={handleSubmit} className="mt-3" style={{ maxWidth: 500 }}>
        <div className="mb-3">
          <label className="form-label">Имя</label>
          <input type="text" className="form-control" required value={name} onChange={e => setName(e.target.value)} />
        </div>
        <div className="mb-3">
          <label className="form-label">Адрес</label>
          <input type="text" className="form-control" required value={address} onChange={e => setAddress(e.target.value)} />
        </div>
        {error && <div className="alert alert-danger">{error}</div>}
        <button type="submit" className="btn btn-primary" disabled={saving}>
          {saving ? 'Сохранение...' : 'Сохранить'}
        </button>
      </form>
    </div>
  );
};

export default AddClient;
