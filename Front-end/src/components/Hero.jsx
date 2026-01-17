import React from 'react';

export default function Hero() {
    return (
        <div style={styles.container}>
            <h1 style={styles.title}>
                Acompanhe o seu time do <span style={{ color: '#FD5656' }}>coração</span> aqui!
            </h1>
            <img
                src="/IMG/estadio.jpg"
                alt="Estádio"
                style={styles.image}
            />
            { }
        </div>
    );
}

const styles = {
    container: {
        position: 'relative',
        top: 40,
        textAlign: 'center',
        height: '400px',
        overflow: 'hidden',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center'
    },
    title: {
        position: 'relative',
        bottom: 50,
        zIndex: 2,
        fontSize: '40px',
        color: '#FFFF',
        textShadow: '2px 2px 4px rgba(0,0,0,0.7)',
        margin: 0
    },
    image: {
        position: 'absolute',
        top: 0,
        left: 0,
        width: '100%',
        height: '100%',
        objectFit: 'cover',
        zIndex: 1,
        filter: 'brightness(20%)'
    }
};

