import React from 'react';
import {Remarkable} from 'remarkable';

export default function MarkdownContainer(props) {
    const md = new Remarkable();

    /**
     * Replaces markdown markup with actual fixed text
     */
    function getRawMarkup() {
        return { __html: md.render(this.props.children) };
    }

    return (
        <div className="content" dangerouslySetInnerHTML={getRawMarkup()}></div>
    );
}
