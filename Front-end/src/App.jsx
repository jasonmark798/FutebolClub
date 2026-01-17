import React from 'react';
import { Routes, Route } from 'react-router-dom';
import Header from './components/Header';
import Hero from './components/Hero';
import TeamList from './components/TeamList';
import Login from './components/Login';
import './App.css';

function App() {
  return (
    <div className="App">
      <Routes>
        <Route path="/" element={
          <>
            <Header />
            <Hero />
            <main>
              <TeamList />
            </main>
            <footer style={styles.footer}>
              <p style={{ textAlign: 'center', color: 'white', padding: '20px' }}>© 2026 FutebolClub</p>
            </footer>
          </>
        } />
        <Route path="/login" element={<Login />} />
      </Routes>
    </div>
  );
}

const styles = {
    footer: {
        marginTop: 200,
        borderTop: '2px solid white'
    }
}



export default App;
