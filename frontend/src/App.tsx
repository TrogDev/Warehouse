import React from 'react';
import './App.css';
import Sidebar from './components/Sidebar';
import { Routes, Route } from 'react-router-dom';
import Balances from './pages/Balances';
import Incomings from './pages/Incomings';
import Outgoing from './pages/Outgoings';
import Units from './pages/Units';
import Clients from './pages/Clients';
import Resources from './pages/Resources';
import AddUnit from './pages/AddUnit';
import EditUnit from './pages/EditUnit';
import AddClient from './pages/AddClient';
import EditClient from './pages/EditClient';
import AddResource from './pages/AddResource';
import EditResource from './pages/EditResource';
import EditIncoming from './pages/EditIncoming';
import AddIncoming from './pages/AddIncoming';
import AddOutgoing from './pages/AddOutgoing';
import EditOutgoing from './pages/EditOutgoing';

function App() {
  return (
    <div className="App">
      <div className="d-flex">
        <Sidebar />
        <Routes>
          <Route path="/balances" element={<Balances />} />
          <Route path="/incomings" element={<Incomings />} />
          <Route path="/incomings/add" element={<AddIncoming />} />
          <Route path="/incomings/:id" element={<EditIncoming />} />
          <Route path="/outgoings" element={<Outgoing />} />
          <Route path="/outgoings/add" element={<AddOutgoing />} />
          <Route path="/outgoings/:id" element={<EditOutgoing />} />
          <Route path="/clients" element={<Clients />} />
          <Route path="/clients/add" element={<AddClient />} />
          <Route path="/clients/:id" element={<EditClient />} />
          <Route path="/units" element={<Units />} />
          <Route path="/units/add" element={<AddUnit />} />
          <Route path="/units/:id" element={<EditUnit />} />
          <Route path="/resources" element={<Resources />} />
          <Route path="/resources/add" element={<AddResource />} />
          <Route path="/resources/:id" element={<EditResource />} />
        </Routes>
      </div>
    </div>
  );
}

export default App;
