import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import Client from '../models/Client';

const Clients: React.FC = () => {
  const [clients, setClients] = useState<Client[]>([]);
  const [filteredStatus, setFilteredStatus] = useState<1 | 2>(1);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetch('/api/clients')
      .then((res) => res.json())
      .then((data: Client[]) => {
        setClients(data);
        setLoading(false);
      })
      .catch((err) => {
        console.error('Ошибка загрузки:', err);
        setLoading(false);
      });
  }, []);

  const filteredClients = clients.filter(c => c.status === filteredStatus);

  return (
    <div className="container mt-4">
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h2>Клиенты</h2>
        <div>
          <Link to="/clients/add" className="btn btn-primary me-2">Добавить</Link>
          <button
            className="btn btn-outline-secondary"
            onClick={() => setFilteredStatus(filteredStatus === 1 ? 2 : 1)}
          >
            {filteredStatus === 1 ? 'К архиву' : 'К рабочим'}
          </button>
        </div>
      </div>

      {loading ? (
        <p>Загрузка...</p>
      ) : (
        <table className="table table-bordered table-striped">
          <thead className="table-light">
            <tr>
              <th>ID</th>
              <th>Имя</th>
              <th>Адрес</th>
              <th> </th>
            </tr>
          </thead>
          <tbody>
            {filteredClients.length === 0 ? (
              <tr><td colSpan={4} className="text-center">Нет данных</td></tr>
            ) : (
              filteredClients.map(client => (
                <tr key={client.id}>
                  <td>{client.id}</td>
                  <td className='w-50'>{client.name}</td>
                  <td className="w-50">{client.address}</td>
                  <td>
                    <Link to={`/clients/${client.id}`} className="btn btn-primary d-inline">Редактировать</Link>
                  </td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default Clients;
