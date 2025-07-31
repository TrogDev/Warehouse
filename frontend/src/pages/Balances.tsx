import React, { useEffect, useState } from 'react';
import Balance from '../models/Balance';
import Resource from '../models/Resource';
import Unit from '../models/Unit';

const Balances: React.FC = () => {
  const [balances, setBalances] = useState<Balance[]>([]);
  const [resources, setResources] = useState<Resource[]>([]);
  const [units, setUnits] = useState<Unit[]>([]);

  const [selectedResources, setSelectedResources] = useState<number[]>([]);
  const [selectedUnits, setSelectedUnits] = useState<number[]>([]);

  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    Promise.all([
      fetch('/api/balances'),
      fetch('/api/resources'),
      fetch('/api/units'),
    ]).then(async ([balancesRes, resourcesRes, unitsRes]) => {
      const balancesData = await balancesRes.json();
      const resourcesData = await resourcesRes.json();
      const unitsData = await unitsRes.json();

      setBalances(balancesData);
      setResources(resourcesData);
      setUnits(unitsData);
      setLoading(false);
    });
  }, []);

  const handleFilter = async () => {
    const params = new URLSearchParams();

    selectedResources.forEach(r => params.append("resources", r.toString()));
    selectedUnits.forEach(u => params.append("units", u.toString()));

    setLoading(true);

    const response = await fetch(`/api/balances?${params.toString()}`);
    const data = await response.json();
    setBalances(data);

    setLoading(false);
  };

  const getResourceName = (id: number) => resources.find(r => r.id === id)?.name || `ID ${id}`;
  const getUnitName = (id: number) => units.find(u => u.id === id)?.name || `ID ${id}`;

  if (loading) return <div className="container mt-4">Загрузка...</div>;

  return (
    <div className="container mt-4">
      <h2>Баланс ресурсов</h2>

      <div className="row mb-3">
        <div className="col-md-6">
          <label className="form-label">Ресурсы</label>
          <select
            multiple
            className="form-select"
            value={selectedResources.map(String)}
            onChange={(e) => {
              const selected = Array.from(e.target.selectedOptions, opt => Number(opt.value));
              setSelectedResources(selected);
            }}
          >
            {resources.map(r => (
              <option key={r.id} value={r.id}>{r.name}</option>
            ))}
          </select>
        </div>
        <div className="col-md-6">
          <label className="form-label">Единицы измерения</label>
          <select
            multiple
            className="form-select"
            value={selectedUnits.map(String)}
            onChange={(e) => {
              const selected = Array.from(e.target.selectedOptions, opt => Number(opt.value));
              setSelectedUnits(selected);
            }}
          >
            {units.map(u => (
              <option key={u.id} value={u.id}>{u.name}</option>
            ))}
          </select>
        </div>
      </div>

      <button className="btn btn-primary mb-3" onClick={handleFilter}>
        Применить фильтры
      </button>

      <table className="table table-bordered">
        <thead className="table-light">
          <tr>
            <th>Ресурс</th>
            <th>Ед. изм.</th>
            <th>Количество</th>
          </tr>
        </thead>
        <tbody>
          {balances.length === 0 ? (
            <tr>
              <td colSpan={3} className="text-center">Нет данных</td>
            </tr>
          ) : (
            balances.map((bal, index) => (
              <tr key={index}>
                <td>{getResourceName(bal.resourceId)}</td>
                <td>{getUnitName(bal.unitId)}</td>
                <td>{bal.quantity}</td>
              </tr>
            ))
          )}
        </tbody>
      </table>
    </div>
  );
};

export default Balances;
