import { useState, useEffect } from 'react';

const Transactions = () => {
    const [transactions, setTransactions] = useState([]);

    useEffect(() => {
        const fetchTransactions = async () => {
            try {
                const response = await fetch('/api/transactions');
                const data = await response.json();
                setTransactions(data);
            } catch (error) {
                console.error('Error fetching transactions:', error);
            }
        };

        fetchTransactions();
    }, []);

    return (
        <div>
            <h1>Transactions</h1>
            <table>
                <thead>
                    <tr>
                        <th>Transaction ID</th>
                        <th>Transaction Type</th>
                        <th>Customer Name</th>
                        <th>Customer Account Number</th>
                        <th>Date and Time</th>
                    </tr>
                </thead>
                <tbody>
                    {transactions.map((transaction) => (
                        <tr key={transaction.id}>
                            <td>{transaction.id}</td>
                            <td>{transaction.type}</td>
                            <td>{transaction.customerName}</td>
                            <td>{transaction.accountNumber}</td>
                            <td>{transaction.dateTime}</td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
};

export default Transactions;