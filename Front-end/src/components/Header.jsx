import React from 'react';
import { Link } from 'react-router-dom';
import '../App.css';

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
        position: 'sticky', // Changed from fixed to sticky
        top: 0,
        width: '100%',
        zIndex: 100, // Increased z-index
        backgroundColor: '#09091A',
        color: '#FFFF',
        boxShadow: '0 2px 10px rgba(0,0,0,0.5)' // Added shadow for visibility
    },
    logo: {
        fontSize: '24px',
        marginRight: 'auto',
        fontFamily: 'PT Sans, sans-serif'
    },
    nav: {
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
