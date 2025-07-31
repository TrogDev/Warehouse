import React, { useEffect, useState } from 'react';
import Incoming from '../models/Incoming';
import Resource from '../models/Resource';
import Unit from '../models/Unit';
import { Link } from 'react-router-dom';

const Incomings: React.FC = () => {
  const [incomings, setIncomings] = useState<Incoming[]>([]);
  const [filteredIncomings, setFilteredIncomings] = useState<Incoming[]>([]);
  const [loading, setLoading] = useState(true);

  const [dateFrom, setDateFrom] = useState('');
  const [dateTo, setDateTo] = useState('');
  const [selectedNumbers, setSelectedNumbers] = useState<string[]>([]);
  const [selectedResources, setSelectedResources] = useState<string[]>([]);
  const [selectedUnits, setSelectedUnits] = useState<string[]>([]);

  const [resources, setResources] = useState<Resource[]>([]);
  const [units, setUnits] = useState<Unit[]>([]);

  useEffect(() => {
    Promise.all([
      fetch('/api/incomings').then(res => res.json()),
      fetch('/api/resources').then(res => res.json()),
      fetch('/api/units').then(res => res.json())
    ])
      .then(([incomingsData, resourcesData, unitsData]) => {
        setIncomings(incomingsData);
        setFilteredIncomings(incomingsData);
        setResources(resourcesData);
        setUnits(unitsData);
        setLoading(false);
      })
  }, []);

  const handleFilter = async () => {
    const params = new URLSearchParams();

    if (dateFrom) params.append('dateFrom', dateFrom);
    if (dateTo) params.append('dateTo', dateTo);
    selectedNumbers.forEach(n => params.append("numbers", n));
    selectedResources.forEach(r => params.append("resources", r));
    selectedUnits.forEach(u => params.append("units", u));

    setLoading(true);

    const response = await fetch(`/api/incomings?${params.toString()}`);
    const data = await response.json();
    setFilteredIncomings(data);

    setLoading(false);
  }

  return (
    <div className="container mt-4">
      <h2>Поступления</h2>

      <div className="card p-3 mb-4">
        <div className="row g-3">
          <div className="col-md-3">
            <label className="form-label">С даты</label>
            <input
              type="date"
              className="form-control"
              value={dateFrom}
              onChange={(e) => setDateFrom(e.target.value)}
            />
          </div>
          <div className="col-md-3">
            <label className="form-label">По дату</label>
            <input
              type="date"
              className="form-control"
              value={dateTo}
              onChange={(e) => setDateTo(e.target.value)}
            />
          </div>
          <div className="col-md-3">
            <label className="form-label">Номер поступления</label>
            <select
              multiple
              className="form-select"
              value={selectedNumbers}
              onChange={(e) =>
                setSelectedNumbers(Array.from(e.target.selectedOptions, o => o.value))
              }
            >
              {incomings.map((incoming) => (
                <option key={incoming.id} value={incoming.number}>{incoming.number}</option>
              ))}
            </select>
          </div>
          <div className="col-md-3">
            <label className="form-label">Ресурс</label>
            <select
              multiple
              className="form-select"
              value={selectedResources}
              onChange={(e) =>
                setSelectedResources(Array.from(e.target.selectedOptions, o => o.value))
              }
            >
              {resources.map((r) => (
                <option key={r.id} value={r.id}>{r.name}</option>
              ))}
            </select>
          </div>
          <div className="col-md-3">
            <label className="form-label">Единица измерения</label>
            <select
              multiple
              className="form-select"
              value={selectedUnits}
              onChange={(e) =>
                setSelectedUnits(Array.from(e.target.selectedOptions, o => o.value))
              }
            >
              {units.map((u) => (
                <option key={u.id} value={u.id}>{u.name}</option>
              ))}
            </select>
          </div>
        </div>

        <div className="mt-3 d-flex gap-2">
          <button className="btn btn-primary" onClick={handleFilter}>Применить фильтры</button>
          <Link to="/incomings/add" className="btn btn-success">Добавить</Link>
        </div>
      </div>

      {loading && <p>Загрузка...</p>}

      {!loading && filteredIncomings.length === 0 && (
        <p>Нет данных</p>
      )}

      {!loading && filteredIncomings.length > 0 && (
        <table className="table table-bordered">
          <thead className="table-light">
            <tr>
              <th>ID</th>
              <th>Номер</th>
              <th>Дата</th>
              <th>Товары</th>
              <th> </th>
            </tr>
          </thead>
          <tbody>
            {filteredIncomings.map((inc) => (
              <React.Fragment key={inc.id}>
                <tr>
                  <td>{inc.id}</td>
                  <td>{inc.number}</td>
                  <td className='text-nowrap'>{inc.date}</td>
                  <td className='w-100'>
                    <table className="table table-sm table-bordered mb-0">
                      <thead className="table-light">
                        <tr>
                          <th>Ресурс</th>
                          <th>Ед. изм.</th>
                          <th>Кол-во</th>
                        </tr>
                      </thead>
                      <tbody>
                        {inc.items.map((item) => (
                          <tr key={item.id}>
                            <td>{item.resource.name}</td>
                            <td>{item.unit.name}</td>
                            <td>{item.quantity}</td>
                          </tr>
                        ))}
                      </tbody>
                    </table>
                  </td>
                  <td><Link to={`/incomings/${inc.id}`} className="btn btn-primary d-inline">Редактировать</Link></td>
                </tr>
              </React.Fragment>
            ))}
          </tbody>
        </table>
      )}
    </div>
  );
};

export default Incomings;
