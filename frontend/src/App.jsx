import './App.css'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import Login from './components/Login'
import ResetRequest from './components/reset_request'
import Dashboard from './components/Dashboard'
import Register from './components/Register'
import AdminDashboard from './components/admin/admin_dashboard'
import FingerprintRegister from './components/fingerprint_register'

function App() {
  
  return (
    <Router>
      <Routes>
      <Route path="/login" element={<Login />} />
      <Route exact path="/reset-request" element={<ResetRequest />} />
      <Route exact path="/dashboard" element={<Dashboard />} />
      <Route exact path="/register" element={<Register />} />
      <Route exact path="/admin" element={<AdminDashboard />} />
      <Route exact path="/fingerprint-register" element={<FingerprintRegister />} />
      </Routes>
    </Router>
  )
}

export default App
