import React from 'react';
import {Link} from "react-router-dom";

const Home = () => {
    return (
        <div>
            <h1>Welcome to Psychologies!</h1>
            <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Phasellus lacinia massa luctus diam vestibulum
                imperdiet. Donec porta consequat dolor nec ultrices. Sed pulvinar ante ac elementum lacinia.</p>

            <hr/>
            <h1><Link to='/tests/1' className="btn btn-primary btn-lg active" role="button" aria-pressed="true">Start personality test</Link></h1>
        </div>
    );
};

export default Home;
