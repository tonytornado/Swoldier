import React, { useState } from 'react';
import MarkdownContainer from '../../../components/modules/MarkdownContainer';
import { InputStack } from './Post';

/**
 * Renders a reply with following replies
 *
 * @param {Array} props A reply, of course.
 */

export function Reply(props) {
    const [reply, setReply] = useState(comment.replies);

    if (reply) {
        return (
            <article key={reply.postId}>
                <MarkdownContainer props={reply.text} />
                <div>
                    <small>{reply.postDate} {reply.Edited ?? "Edited"}</small>
                </div>
                <div>
                    <button className="btn btn-sm" value="Reply" />
                </div>
            </article>
        );
    }

    return (
        <InputStack type={"Reply"} />
    );
}
