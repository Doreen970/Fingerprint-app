import { useState, useEffect } from 'react';
import axios from 'axios';
import CustomerCard from './Customers';
import { Pagination } from '@mui/material';
import { useNavigate } from 'react-router-dom';


const Dashboard = () => {
    const [customers, setCustomers] = useState([
        {
            id: 1,
            name: 'John Doe',
            accountNumber: '1234567890',
        },
        {
            id: 2,
            name: 'Jane Smith',
            accountNumber: '0987654321',
        },
    ]);
    const [searchTerm, setSearchTerm] = useState('');
    const [currentPage, setCurrentPage] = useState(1);
    const [rowsPerPage] = useState(20);
    const [selectedCustomer, setSelectedCustomer] = useState(null);

    const navigate = useNavigate();

    useEffect(() => {
        axios.get('endpoint-here')
            .then(response => {
                setCustomers(response.data);
            });
    }, []);

    const handleSearchChange = (event) => {
        setSearchTerm(event.target.value);
    };

    const handleRegisterCustomer = () => {
        navigate("/register")
    };

    const handlePageChange = (event, value) => {
        setCurrentPage(value);
    };

    const handleClickRow = (customer) => {
        setSelectedCustomer(customer);
    };

    const filteredCustomers = Array.isArray(customers) ? customers.filter(customer => 
        customer.name.toLowerCase().includes(searchTerm.toLowerCase())
    ): [];

    const indexOfLastRow = currentPage * rowsPerPage;
    const indexOfFirstRow = indexOfLastRow - rowsPerPage;
    const currentRows = filteredCustomers.slice(indexOfFirstRow, indexOfLastRow);

    return (
        <div>
            <input
                type="text"
                placeholder="Search by ID"
                value={searchTerm}
                onChange={handleSearchChange}
            />

            <button onClick={handleRegisterCustomer}>Register Customer</button>

            {selectedCustomer && <CustomerCard customer={selectedCustomer} />}

            <ul>
                {currentRows.map((customer) => (
                    <li key={customer.id} onClick={() => handleClickRow(customer)}>
                        {customer.name} - {customer.id} - {customer.accountNumber}
                    </li>
                ))}
            </ul>

            {filteredCustomers.length > rowsPerPage && (
                <Pagination count={Math.ceil(filteredCustomers.length / rowsPerPage)} page={currentPage} onChange={handlePageChange} />
            )}
        </div>
    );
};

export default Dashboard;