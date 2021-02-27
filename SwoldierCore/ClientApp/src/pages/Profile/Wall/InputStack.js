import React, { useState } from 'react';
import authService from '../../../components/api-authorization/AuthorizeService';


export function InputStack(props) {
    const [text, setText] = useState("Write something...");

    const modifier = Math.random();

    const processEntry = (type) => {
        var token = authService.getAccessToken;
        fetch(`api/Wall/${type}/`, {
            headers: !token ? {} : {
                'Authorization': `Bearer ${token}`
            },
            method: "POST",
            body: $(`#inputStack_${modifier}`).serialize()
        });
    };

    return (
        <>
            <form id={`inputStack_${modifier}`}>
                <textarea
                    id="markdown-content"
                    name="entryText"
                    defaultValue={text} />
                <br />
                <button className="btn btn-sm" value="Reply" onClick={processEntry(props.type)} />
            </form>
        </>
    );
}
