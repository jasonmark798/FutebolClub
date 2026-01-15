import React from 'react';

export default function Hero() {
    return (
        <div style={styles.container}>
            <div className="container" style={styles.content}>
                <h1 style={styles.title}>
                    Acompanhe o seu time do <span style={{ color: '#FD5656' }}>coração</span> aqui!
                </h1>
            </div>
            <img
                src="/IMG/estadio.jpg"
                alt="Estádio"
                style={styles.image}
            />
        </div>
    );
}

const styles = {
    container: {
        position: 'relative',
        textAlign: 'center',
        height: '500px', // Increased height
        overflow: 'hidden',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center'
    },
    content: {
        position: 'relative',
        zIndex: 2,
        width: '100%'
    },
    title: {
        position: 'relative',
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
        filter: 'brightness(30%)'
    }
};
