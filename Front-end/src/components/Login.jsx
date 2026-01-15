import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import './Login.css';

export default function Login() {
    const [email, setEmail] = useState('');
    const [senha, setSenha] = useState('');
    const [error, setError] = useState('');
    const navigate = useNavigate();

    const handleLogin = async (e) => {
        e.preventDefault();
        setError('');

        try {
            const response = await axios.post('http://localhost:5115/login', {
                email,
                senha
            });

            if (response.data && response.data.token) {
                localStorage.setItem('token', response.data.token);
                localStorage.setItem('user', JSON.stringify({
                    nome: response.data.nome,
                    email: response.data.email
                }));
                navigate('/'); // Redirect to home on success
            }
        } catch (err) {
            console.error(err);
            setError('Login falhou. Verifique suas credenciais.');
        }
    };

    return (
        <div className="login-container">
            <div className="login-card">
                <h2 className="login-title">Acesso <span style={{ color: '#FAEC81' }}>Restrito</span></h2>

                <form onSubmit={handleLogin}>
                    <div className="input-group">
                        <label>Email</label>
                        <input
                            type="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            placeholder="ex: admin@futebol.com"
                            required
                        />
                    </div>

                    <div className="input-group">
                        <label>Senha</label>
                        <input
                            type="password"
                            value={senha}
                            onChange={(e) => setSenha(e.target.value)}
                            placeholder="********"
                            required
                        />
                    </div>

                    {error && <p className="error-message">{error}</p>}

                    <button type="submit" className="login-btn">
                        Entrar
                    </button>
                </form>

                <p className="login-footer">
                    Ainda não tem conta? <span style={{ color: '#FD5656' }}>Cadastre-se</span>
                </p>
            </div>
        </div>
    );
}
