import React, { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import Resource from '../models/Resource';

const Resources: React.FC = () => {
  const [resources, setResources] = useState<Resource[]>([]);
  const [filteredState, setFilteredState] = useState<1 | 2>(1);
  const [loading, setLoading] = useState<boolean>(true);

  useEffect(() => {
    fetch('/api/resources')
      .then((res) => res.json())
      .then((data: Resource[]) => {
        setResources(data);
        setLoading(false);
      });
  }, []);

  const filteredResources = resources.filter((r) => r.status === filteredState);

  return (
    <div className="container mt-4">
      <div className="d-flex justify-content-between align-items-center mb-3">
        <h2>Ресурсы</h2>
        <div>
          <Link to="/resources/add" className="btn btn-primary me-2">Добавить</Link>
          <button
            className="btn btn-outline-secondary"
            onClick={() => setFilteredState(filteredState === 1 ? 2 : 1)}
          >
            {filteredState === 1 ? 'К архиву' : 'К рабочим'}
          </button>
        </div>
      </div>

      {loading ? (
        <p>Загрузка...</p>
      ) : (
        <table className="table table-striped table-bordered">
          <thead className="table-light">
            <tr>
              <th>ID</th>
              <th>Наименование</th>
              <th> </th>
            </tr>
          </thead>
          <tbody>
            {filteredResources.length === 0 ? (
              <tr>
                <td colSpan={3} className="text-center">Нет данных</td>
              </tr>
            ) : (
              filteredResources.map((resource) => (
                <tr key={resource.id}>
                  <td>{resource.id}</td>
                  <td className='w-100'>{resource.name}</td>
                  <td><Link className='btn btn-primary d-inline' to={`/resources/${resource.id}`}>Редактировать</Link></td>
                </tr>
              ))
            )}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default Resources;
