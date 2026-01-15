import React, { useState } from 'react';
import './PlayerCard.css'; // We will create this CSS

export default function PlayerCard({ player }) {
    const [isFlipped, setIsFlipped] = useState(false);

    return (
        <div
            className={`player-card ${isFlipped ? 'flipped' : ''}`}
            onClick={() => setIsFlipped(!isFlipped)}
        >
            <div className="card-inner">
                <div className="card-front">
                    <img
                        src={player.foto || 'https://via.placeholder.com/150'}
                        alt={player.nome}
                        className="player-photo"
                    />
                    <div className="player-info">
                        <h3>{player.nome}</h3>
                        <p>{player.posicao}</p>
                    </div>
                </div>
                <div className="card-back">
                    <h3>{player.nome}</h3>
                    <p><strong>Posição:</strong> {player.posicao}</p>
                    <p><strong>Id:</strong> {player.id}</p>
                    {/* Add more info if available */}
                </div>
            </div>
        </div>
    );
}
