import React from 'react';
import { Link } from 'react-router-dom';
import '../App.css';
import './Hover.css';

export default function Header() {
    return (
        <header style={styles.header}>
            <h1 style={styles.logo}>FutebolClub</h1>
            <nav style={styles.nav}>
                <a href="/#" style={styles.link}>Placar</a>
                <a href="/#times" style={styles.link}>Times</a>
                <Link to="/login">
                    <button style={styles.button}>Login</button>
                </Link>
            </nav>
        </header>
    );
}

const styles = {
    header: {
        display: 'flex',
        alignItems: 'center',
        padding: '10px 50px',
        position: 'fixed', // sticky or fixed?
        top: 0,
        width: '100%',
        zIndex: 100, // Increased z-index
        color: '#FFFF',

    },
    logo: {
        fontSize: '24px',
        marginRight: 'auto',
        fontFamily: 'PT Sans, sans-serif'
    },
    nav: {
        marginRight: '150px',
        display: 'flex',
        gap: '20px',
        alignItems: 'center'
    },
    link: {
        color: '#FFFF',
        textDecoration: 'none',
        fontSize: '18px',
        fontFamily: 'PT Sans, sans-serif'
    },
    button: {
        marginLeft: '20px',
        borderRadius: '20px',
        border: 'none',
        width: '80px',
        height: '30px',
        cursor: 'pointer',
        fontWeight: 'bold'
    }
};
