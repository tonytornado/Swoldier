import React from "react";

import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faCircleNotch, faSpinner } from '@fortawesome/free-solid-svg-icons'

// export function Loader() {
//     return <faSpinner />
//     // return "Not here";
// }


export function Loader() {
    return <FontAwesomeIcon icon={faSpinner} size={"2x"} spin />;
}

export function LargeLoader() {
    return <FontAwesomeIcon icon={faCircleNotch} size={"5x"} spin />
}

export function LoginLoader() {
    return <div className="text-center">
        <h3>Processing Login</h3>
        <FontAwesomeIcon icon={faCircleNotch} size={"6x"} spin />
    </div>
}