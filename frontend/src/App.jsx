import './App.css'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import Login from './components/Login'
import ResetRequest from './components/reset_request'
import Dashboard from './components/Dashboard'
import Register from './components/Register'

function App() {

  return (
    <Router>
      <Routes>
        <Route path="/login" element={<Login />} />
        <Route exact path="/reset-request" element={<ResetRequest />} />
        <Route exact path="/dashboard" element={<Dashboard />} />
        <Route exact path="/register" element={<Register />} />
      </Routes>
    </Router>
  )
}

export default App
