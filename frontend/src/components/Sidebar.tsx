import React from 'react';
import { Link } from 'react-router-dom';

const Sidebar: React.FC = () => {
  return (
    <div className="d-flex flex-column bg-light vh-100 p-3" style={{ width: '250px' }}>
      <h4 className="mb-4">Управление складом</h4>

      <div>
        <h6 className="text-uppercase text-muted">Склад</h6>
        <ul className="nav flex-column mb-3">
          <li className="nav-item">
            <Link to="/balances" className="nav-link">Баланс</Link>
          </li>
          <li className="nav-item">
            <Link to="/incomings" className="nav-link">Поступления</Link>
          </li>
          <li className="nav-item">
            <Link to="/outgoings" className="nav-link">Отгрузки</Link>
          </li>
        </ul>

        <h6 className="text-uppercase text-muted">Справочники</h6>
        <ul className="nav flex-column">
          <li className="nav-item">
            <Link to="/clients" className="nav-link">Клиенты</Link>
          </li>
          <li className="nav-item">
            <Link to="/units" className="nav-link">Единицы измерения</Link>
          </li>
          <li className="nav-item">
            <Link to="/resources" className="nav-link">Ресурсы</Link>
          </li>
        </ul>
      </div>
    </div>
  );
};

export default Sidebar;
