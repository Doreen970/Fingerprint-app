import './App.css'
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom'
import Login from './components/Login'
import ResetRequest from './components/reset_request'


function App() {
  
  return (
    <Router>
      <Routes>
      <Route path="/login" element={<Login />} />
      <Route exact path="/reset-request" element={<ResetRequest />} />
      
      </Routes>
    </Router>
  )
}

export default App
